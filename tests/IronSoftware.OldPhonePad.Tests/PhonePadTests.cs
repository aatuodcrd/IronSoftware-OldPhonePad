using NUnit.Framework;
using IronSoftware.OldPhonePad;
using System;

namespace IronSoftware.OldPhonePad.Tests
{
    public class PhonePadTests
    {
        [Test]
        public void OldPhonePad_BasicInput_ReturnsExpectedOutput()
        {
            Assert.That(PhonePad.OldPhonePad("33#"), Is.EqualTo("E"));
            Assert.That(PhonePad.OldPhonePad("22#"), Is.EqualTo("B"));
        }

        [Test]
        public void OldPhonePad_Backspace_RemovesCharacters()
        {
            // B (22) -> B deleted (*) -> 2 (ABC2[3]=2) -> Output: 2 -- Wait, logic check:
            // 227*# 
            // 22 -> B
            // 7 -> P
            // * -> Backspace (removes P)
            // # -> Commit
            // Result: B
            Assert.That(PhonePad.OldPhonePad("227*#"), Is.EqualTo("B"));
            
            // 4433555 555666#
            // 44 -> H
            // 33 -> E
            // 555 -> L
            // ' ' -> Commit
            // 555 -> L
            // 666 -> O
            // Result: HELLO
            Assert.That(PhonePad.OldPhonePad("4433555 555666#"), Is.EqualTo("HELLO"));
        }
        
        [Test]
        public void OldPhonePad_ComplexBackspace_ReturnsCorrectResult()
        {
             // 8 88777444666*664#
             // 8 -> T
             // 88 -> U
             // 777 -> R
             // 444 -> I
             // 666 -> N
             // * -> delete N -> TURI
             // 66 -> N
             // 4 -> G
             // # -> TURING
             Assert.That(PhonePad.OldPhonePad("8 88777444666*664#"), Is.EqualTo("TURING"));
        }

        [Test]
        public void OldPhonePad_Pause_CommitsCharacter()
        {
            // 2->A, space->commit, 2->A => AA
            Assert.That(PhonePad.OldPhonePad("2 2#"), Is.EqualTo("AA"));
        }
        
        [Test]
        public void OldPhonePad_SpaceEncoding_ReturnsSpace()
        {
            // 0 -> Space
            Assert.That(PhonePad.OldPhonePad("0#"), Is.EqualTo(" "));
            Assert.That(PhonePad.OldPhonePad("2222 08#"), Is.EqualTo("2 T")); 
            // 2222 -> 2 (4th char in ABC2)
            // 0 -> Space
            // 8 -> T
             Assert.That(PhonePad.OldPhonePad("2222#"), Is.EqualTo("2"));
        }

        [Test]
        public void OldPhonePad_Cycling_WorksCorrectly()
        {
             // 7 -> P
             // 77 -> Q
             // 777 -> R
             // 7777 -> S
             // 77777 -> 7
             // 777777 -> P (cycle back)
             Assert.That(PhonePad.OldPhonePad("777777#"), Is.EqualTo("P"));
        }

        [Test]
        public void OldPhonePad_InvalidInput_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => PhonePad.OldPhonePad("234"));
            Assert.That(ex.Message, Is.EqualTo("Error: Input must end with a send button '#'."));
            
            Assert.Throws<ArgumentException>(() => PhonePad.OldPhonePad(null));
            Assert.Throws<ArgumentException>(() => PhonePad.OldPhonePad(""));
        }
    }
}
